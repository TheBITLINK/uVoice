import PIXI from 'pixi.js';

/**
 * Note Object Renderer
 */
export default class NoteObjectRenderer {
  /**
   * Instantiates a new NoteObjectRenderer.
   * @param {EditorCanvas} canvas - Parent canvas.
   */
  constructor(canvas) {
    this.canvas = canvas;
    this.renderer = canvas.renderer;
    this.editor = canvas.editor;
    this.project = this.editor.project;
    this.stage = new PIXI.Container();
    this.shouldRedraw = true;
    this.refreshLast();
  }

  /** Called once per frame */
  update() {
    this.shouldRedraw = this.shouldRedraw
      || this.last.viewX !== this.editor.viewX
      || this.last.zoomFactor !== this.editor.zoomFactor
      || this.last.noteHeight !== this.editor.noteHeight
      || this.project.noteObjects.length !== this.last.count;
    if (!this.shouldRedraw) return;
    this.stage.removeChildren();
    for (const note of this.project.noteObjects) {
      // Don't draw the note if it's out of view.
      const shouldDraw =
        this.editor.viewX <= (note.offset + note.width) * this.editor.zoomFactor
        && this.editor.viewX + this.renderer.width >= note.offset * this.editor.zoomFactor;
      if (shouldDraw) this.drawNoteObject(note);
    }
    this.shouldRedraw = false;
    this.refreshLast();
  }

  refreshLast() {
    this.last = {
      viewX: this.editor.viewX,
      zoomFactor: this.editor.zoomFactor,
      noteHeight: this.editor.noteHeight,
      count: this.project.noteObjects.length,
    };
  }

  /**
   * Draws a NoteObject in the canvas.
   * @param {NoteObject} note
   */
  drawNoteObject(note) {
    const graphic = new PIXI.Graphics();
    // (Temporary) draw just a rectangle where the note should be.
    graphic.beginFill(µV.common.accentColor, 1);
    graphic.drawRoundedRect(
      (note.offset * this.editor.zoomFactor) - this.editor.viewX,
      µV.common.noteIndexes[note.noteName] * this.editor.noteHeight,
      note.width * this.editor.zoomFactor,
      this.editor.noteHeight,
      3
    );
    graphic.endFill();
    this.stage.addChild(graphic);
  }
}
