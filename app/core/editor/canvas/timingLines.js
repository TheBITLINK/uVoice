import PIXI from 'pixi.js';

/**
 * Displays tempo guides over the editor.
 * @property {EditorCanvas} canvas
 * @property {Editor} editor
 * @property {(PIXI.WebGLRenderer|PIXI.CanvasRenderer)} renderer
 * @property {PIXI.Container} stage
 * @property {PIXI.Graphics} lines
 */
export default class TimimngLines {
  /**
   * Creates a new instace of TimimngLines.
   * @param {EditorCanvas} canvas
   */
  constructor(canvas) {
    this.canvas = canvas;
    this.editor = canvas.editor;
    this.renderer = canvas.renderer;
    this.stage = new PIXI.Container();
    this.lines = new PIXI.Graphics();
    this.stage.addChild(this.lines);
  }

  /** Redraw the lines */
  update() {
    // TODO: Efficient way of redrawing lines.
    const lineSpacing = this.editor.zoomFactor * 96;
    const maxLines = Math.floor(this.renderer.width / lineSpacing) + 2;
    const offsetLines = Math.floor(this.editor.viewX / lineSpacing);
    this.lines.clear();

    for (let i = 0; i < maxLines; i++) {
      const isDivisor = !((offsetLines + i) % 4);
      const lineX = (lineSpacing * i) - (this.editor.viewX % lineSpacing);
      this.lines.lineStyle(
        isDivisor ? 2 : 1,
        0xFFFFFF,
        isDivisor ? 0.5 : 0.25
      );

      this.lines.moveTo(lineX, 0);
      this.lines.lineTo(lineX, this.renderer.height);
    }

    this.renderer.render(this.stage);
  }
}
