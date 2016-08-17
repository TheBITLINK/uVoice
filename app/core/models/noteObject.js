/**
 * Represents a note in the timeline.
 * @property {number} offset - The note offset.
 * @property {string} noteName - Note to play.
 * @property {boolean} selected - If the note is selected in the editor.
 */
export default class NoteObject {
  /**
   * Instantiates a new NoteObject
   * @param {Object} data - Initial state.
   */
  constructor(data = {}) {
    this.offset = 0;
    this.width = 12;
    this.noteName = 'C3';
    this.selected = false;
    Object.assign(this, data);
  }
}
