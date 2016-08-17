import $ from 'jquery';

/**
 * µVoice Piano Roll class
 * @property {HTMLElement} el - HTML element.
 * @property {object} playing - Notes being played.
 * @property {HTMLElement[]} notes - Notes elements.
 */
export default class PianoRoll
{
  /**
   * Initializes a new PianoRoll instance.
   * @param {HTMLElement} el - Linked HTML element.
   */
  constructor(editor, el) {
    this.editor = editor;
    this.el = el;
    this.playing = {};
    this.keyEvents();
    this.notes = el.querySelectorAll('.note');
  }

  /** Bind events to the piano keys */
  keyEvents() {
    $(this.el).on('mousedown touchstart', '.pianoKey', e => this.playNote(e.target.parentElement));
    $(this.el).on('mouseup touchend', '.pianoKey', () => this.stopAll());
  }

  /**
   * Start playing a note.
   * @param {(string|HTMLElement)} note - The note.
   */
  playNote(note) {
    const name = note.getAttribute ? note.getAttribute('name') : note;
    // Handle common errors
    if (!µV.common.notes[name]) {
      throw new Error(`${name} is not a valid note.`);
    }
    if (this.playing[name]) {
      throw new Error(`${name} is already being played.`);
    }

    const o = µV.ctx.createOscillator();
    o.type = 'square';
    o.frequency.value = µV.common.notes[name];
    o.connect(µV.ctx.destination);
    o.start(0);
    this.playing[name] = o;
  }

  /**
   * Stop playing a note.
   * @param {(string|HTMLElement)} note - The note.
   */
  stopNote(note) {
    const name = note.getAttribute ? note.getAttribute('name') : note;
    // Handle common errors
    if (!µV.common.notes[name]) {
      throw new Error(`${name} is not a valid note.`);
    }
    if (!this.playing[name]) {
      throw new Error(`${name} is not being played.`);
    }

    this.playing[name].stop(0);
    delete this.playing[name];
  }

  /** Stop all currently playing notes */
  stopAll() {
    for (const note of Object.keys(this.playing)) {
      this.stopNote(note);
    }
  }
}
