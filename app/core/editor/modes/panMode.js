/**
 * Editor Pan Mode
 */
export default class PanMode {
  /**
   * Creates a new instace of PanMode
   * @param {EditorCanvas} canvas
   */
  constructor(canvas) {
    this.canvas = canvas;
    this.editor = canvas.editor;
    this.pointer = canvas.pointer;
    this.panOrigin = [0, 0, 0];
  }

  /** update */
  update() {
    // TODO: Add a kinetic effect.
    if (!this.pointer.held) {
      this.panOrigin = [0, 0, 0];
      return;
    }
    // Horizontal panning
    if (this.panOrigin[0] === 0) this.panOrigin[0] = this.pointer.x;
    if (this.panOrigin[2] === 0) this.panOrigin[2] = this.editor.viewX;
    const offset = this.panOrigin[2] + (this.panOrigin[0] - this.pointer.x);
    if (offset > this.editor.timelineWidth) {
      // Cap scroll at max timeline width
      this.editor.viewX = this.editor.timelineWidth;
    } else if (offset < 0) {
      // Cap scroll at zero
      this.editor.viewX = 0;
    } else {
      this.editor.viewX = offset;
    }
  }
}
