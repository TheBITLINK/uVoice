import $ from 'jquery';
import noteFrequencies from './notes';

/**
 * µVoice Common Functions
 * @property {object} notes - A dictionary to resolve notes into frequencies.
 */
export default class Common {
  constructor() {
    this.notes = noteFrequencies;
  }

  /** Initialize µVoice. */
  init() {
  }

  /**
   * Show a tooltip in the middle of the screen.
   * @param {string} text - Text of the tooltip.
   */
  showTooltip(text) {
    const tooltip = $('<div class="overlayTooltip"></div>')
      .text(text)
      .appendTo('#uv-overlay');

    setTimeout(() => {
      tooltip.remove();
    }, 2100);
  }
}
