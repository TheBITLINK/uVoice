import Vue from 'vue';
import $ from 'jquery';
import PianoRoll from './pianoRoll';

/**
 * µVoice Editor class.
 * @property {PianoRoll} pianoRoll - The PianoRoll of this editor.
 * @property {string}    mode      - Current mode.
 */
export default class Editor {
  /**
   * Create a new editor instance.
   * @param {HTMLElement} el - The element to link to this editor.
   */
  constructor(el) {
    this.pianoRoll = new PianoRoll($('.uv-pianoRoll')[0]);
    this.mode = 'select';
    this.registerEvents();
    this.vue = new Vue({
      data: this,
      el,
      methods: {
        setMode: this.setMode,
      },
    });
  }

  /**
   * Set the editor mode.
   * @param {string} mode - New Mode
   */
  setMode(mode) {
    this.mode = mode;
    µV.common.showTooltip(`${mode} Mode.`);
  }

  /** Register events and handlers. */
  registerEvents() {
    $('#uv-logo').click(() => {
      $('#uv-main-sidebar').sidebar('toggle');
    });
  }
}
