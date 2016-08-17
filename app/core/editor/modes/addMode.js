import PIXI from 'pixi.js';
import { NoteObject } from '../../models';

/**
 * Editor Add Mode
 */
export default class AddMode {
  /**
   * Creates a new instace of AddMode
   * @param {EditorCanvas} canvas
   */
  constructor(canvas) {
    this.canvas = canvas;
    this.editor = canvas.editor;
    this.pointer = canvas.pointer;
    this.highlight = canvas.highlight;
    this.gfx = new PIXI.Graphics();
    this.origin = 0;
    this.resetSelection();
  }

  /** update */
  update() {
    if (!this.pointer.held) {
      if (this.selection.width) {
        this.addSelection();
      }
      this.origin = 0;
      this.gfx.clear();
      return;
    }
    if (this.origin === 0) this.origin = this.pointer.x;

    // Note Object Width
    this.selection.width =
      (Math.round((this.pointer.x - this.origin) / 24) * 24) / this.editor.zoomFactor;
    if (this.pointer.x < this.origin) this.selectionWidth = 0;
    // Note name
    this.selection.noteName = this.highlight.note.getAttribute('name');
    // Note offset
    this.selection.offset =
      this.editor.viewX + ((Math.round(this.origin / 24) * 24) / this.editor.zoomFactor);
    this.gfx.clear();
    this.gfx.lineStyle(3, µV.common.accentColor, 0.8);
    this.gfx.beginFill(µV.common.accentColor, 0.2);
    this.gfx.drawRoundedRect(
      Math.round(this.origin / 24) * 24,
      Math.round(this.highlight.offset * this.editor.noteHeight),
      Math.round((this.pointer.x - this.origin) / 24) * 24,
      this.editor.noteHeight,
      3
    );
    this.gfx.endFill();
    // TODO: Scroll if cursor is near the corner while adding
  }

  addSelection() {
    this.editor.project.noteObjects.push(new NoteObject(this.selection));
    this.editor.updateTimelineWidth();
    this.resetSelection();
  }

  resetSelection() {
    this.selection = {
      noteName: '',
      offset: undefined,
      width: 0,
    };
  }
}
