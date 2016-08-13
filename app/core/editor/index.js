import $ from 'jquery';
import PianoRoll from './pianoRoll';

/**
 * µVoice Editor class.
 * @property {PianoRoll} pianoRoll - The PianoRoll of this editor.
 * @property {string}    mode      - Current mode.
 */
export default class Editor {
  constructor() {
    this.pianoRoll = new PianoRoll($('.uv-pianoRoll')[0]);
    this.mode = 'select';
    this.registerEvents();
  }

  /** Register events and handlers. */
  registerEvents() {
    $('#uv-logo').click(() => {
      $('#uv-main-sidebar').sidebar('toggle');
    });
  }
}
