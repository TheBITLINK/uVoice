import PIXI from 'pixi.js';
import TimingLines from './timingLines';

/**
 * A PIXI renderer for notes.
 * @property {Editor} editor - The parent Editor.
 * @property {(PIXI.WebGLRenderer|PIXI.CanvasRenderer)} renderer - The Renderer.
 * @property {TimingLines} timingLines
 */
export default class EditorCanvas {
  /**
   * Creates a new EditorCanvas instance.
   * @param {Editor} editor - The parent Editor.
   */
  constructor(editor) {
    this.editor = editor;
    this.renderer = PIXI.autoDetectRenderer(0, 0, { transparent: true });
    editor.el.querySelector('.noteContainer').appendChild(this.renderer.view);
    this.updateRendererSize();

    this.timingLines = new TimingLines(this);

    this.registerEvents();
    this.update();
  }

  /** Register Event Handlers. */
  registerEvents() {
    µV.events.on('windowResize', () => this.updateRendererSize());
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
  }
}
