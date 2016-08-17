import PIXI from 'pixi.js';

/**
 * Highlights notes on hover
 * @property {EditorCanvas} canvas
 * @property {Editor} editor
 * @property {(PIXI.WebGLRenderer|PIXI.CanvasRenderer)} renderer
 * @property {PIXI.Container} stage
 * @property {PIXI.Graphics} gfx
 */
export default class NoteHighlight {
  /**
   * Creates a new instace of NoteHighlight.
   * @param {EditorCanvas} canvas
   */
  constructor(canvas) {
    this.canvas = canvas;
    this.editor = canvas.editor;
    this.renderer = canvas.renderer;
    this.pointer = canvas.pointer;
    this.stage = new PIXI.Container();
    this.gfx = new PIXI.Graphics();
    this.note = undefined;
    this.offset = undefined;
    this.lastOffset = undefined;
    this.stage.addChild(this.gfx);
  }

  /** Redraw */
  update() {
    if (this.pointer.y <= 0) {
      this.stage.renderable = false;
      this.note = this.offset = undefined;
      return;
    }
    this.stage.renderable = true;
    this.lastOffset = this.offset;
    this.offset = Math.floor(this.pointer.y / this.editor.noteHeight);
    // Only update if the offset has changed
    if (this.lastOffset === this.offset) return;

    this.note = this.editor.pianoRoll.notes[this.offset];
    // Draw a rectangle over the highlighted note
    this.gfx.clear();
    this.gfx.lineStyle(2, µV.common.accentColor, 0.1);
    this.gfx.drawRect(
      0,
      this.offset * this.editor.noteHeight,
      this.renderer.width,
      this.editor.noteHeight
    );
  }
}
