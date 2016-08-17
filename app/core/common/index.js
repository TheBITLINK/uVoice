import $ from 'jquery';
import { NoteToFreq, NoteToIndex } from './notes';

/**
 * µVoice Common Functions
 * @property {object} notes - A dictionary to resolve notes into frequencies.
 */
export default class Common {
  constructor() {
    this.notes = NoteToFreq;
    this.noteIndexes = NoteToIndex;
    this.accentColor = 0x00AACC;
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
