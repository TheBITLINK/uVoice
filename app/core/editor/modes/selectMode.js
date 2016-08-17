import PIXI from 'pixi.js';

/**
 * Editor Select Mode
 */
export default class SelectMode {
  /**
   * Creates a new instace of SelectMode
   * @param {EditorCanvas} canvas
   */
  constructor(canvas) {
    this.canvas = canvas;
    this.editor = canvas.editor;
    this.pointer = canvas.pointer;
    this.gfx = new PIXI.Graphics();
    this.selectOrigin = [0, 0];
  }

  /** update */
  update() {
    if (!this.pointer.held) {
      this.selectOrigin = [0, 0];
      this.gfx.clear();
      return;
    }
    if (this.selectOrigin[0] === 0) this.selectOrigin[0] = this.pointer.x;
    if (this.selectOrigin[1] === 0) this.selectOrigin[1] = this.pointer.y;
    const width = this.pointer.x - this.selectOrigin[0];
    const height = this.pointer.y - this.selectOrigin[1];
    this.gfx.clear();
    this.gfx.lineStyle(1, µV.common.accentColor, 0.8);
    this.gfx.beginFill(µV.common.accentColor, 0.2);
    this.gfx.drawRect(this.selectOrigin[0], this.selectOrigin[1], width, height);
    this.gfx.endFill();
    // TODO: Scroll if cursor is near the corner while selecting
  }
}
