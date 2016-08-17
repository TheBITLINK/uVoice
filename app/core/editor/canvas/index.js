import PIXI from 'pixi.js';
import $ from 'jquery';
import TimingLines from './timingLines';
import NoteHighlight from './highlight';
import NoteObjectRenderer from './noteObjectRenderer';
import getModeHandlers from '../modes';

/**
 * A PIXI renderer for the editor.
 * @property {Editor} editor - The parent Editor.
 * @property {(PIXI.WebGLRenderer|PIXI.CanvasRenderer)} renderer - The Renderer.
 * @property {TimingLines} timingLines
 * @property {Object} pointer - Pointer information
 */
export default class EditorCanvas {
  /**
   * Creates a new EditorCanvas instance.
   * @param {Editor} editor - The parent Editor.
   */
  constructor(editor) {
    this.editor = editor;
    this.renderer = PIXI.autoDetectRenderer(0, 0, { transparent: true });
    this.pointer = {
      x: 0,
      y: 0,
      held: false,
    };
    this.stage = new PIXI.Container();
    this.timingLines = new TimingLines(this);
    this.highlight = new NoteHighlight(this);
    this.noteObjectRenderer = new NoteObjectRenderer(this);
    this.modes = getModeHandlers(this);

    this.stage.addChild(this.timingLines.stage);
    this.stage.addChild(this.highlight.gfx);
    this.stage.addChild(this.noteObjectRenderer.stage);
    editor.el.querySelector('.noteContainer').appendChild(this.renderer.view);
    this.updateRendererSize();
    this.registerEvents();
    this.update();
  }

  /** Register Event Handlers. */
  registerEvents() {
    // Window Resize
    µV.events.on('windowResize', () => this.updateRendererSize());
    // Mouse Events
    $(this.renderer.view).on('mousemove', e => {
      µV.e = e;
      this.pointer.x = e.offsetX;
      this.pointer.y = e.offsetY - this.renderer.view.offsetTop;
    })
    .on('mouseout', () => {
      this.pointer.x = this.pointer.y = 0;
    })
    .on('mousedown', () => {
      this.pointer.held = true;
    })
    .on('mouseup mouseout', () => {
      this.pointer.held = false;
    });
  }

  /** Update Renderer Size to cover the Piano Roll edit area. */
  updateRendererSize() {
    const noteContainer = this.editor.el.querySelector('.noteContainer');
    const pianoKey = noteContainer.querySelector('.pianoKey');

    this.renderer.resize(
      noteContainer.clientWidth - pianoKey.clientWidth,
      noteContainer.clientHeight
    );
  }

  /** Called every frame */
  update() {
    requestAnimationFrame(() => this.update());
    this.timingLines.update();
    this.highlight.update();
    this.noteObjectRenderer.update();
    // Mode Handlers
    if (this.modes[this.editor.mode]) {
      try {
        this.modes[this.editor.mode].update();
      } catch (ex) {
        //
      }
    }
    this.renderer.render(this.stage);
  }
}
