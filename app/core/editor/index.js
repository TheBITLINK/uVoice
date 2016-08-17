import Vue from 'vue';
import $ from 'jquery';
import EventEmitter from 'events';
import PianoRoll from './pianoRoll';
import EditorCanvas from './canvas';
import { Project } from '../models';

/**
 * µVoice Editor class.
 * @property {PianoRoll} pianoRoll - The PianoRoll of this editor.
 * @property {string} mode      - Current mode.
 * @property {number} viewX     - The current horizontal position of the editor view (and playback).
 * @property {number} zoomFactor - Current horizontal zoom of the editor view.
 * @property {number} timelineWidth - Current width of the timeline.
 * @fires Editor#sidebarToggle
 * @fires Editor#zoomChange
 */
export default class Editor extends EventEmitter {
  /**
   * Create a new editor instance.
   * @param {HTMLElement} el - The element to link to this editor.
   */
  constructor(el) {
    super();
    this.el = el;
    this.mode = 'select';
    this.registerEvents();
    this.viewX = 0;
    this.zoomFactor = 1;
    this.timelineWidth = 100;
    this.noteHeight = 16;

    this.project = new Project();

    this.vue = new Vue({
      data: this,
      el,
      methods: {
        setMode: this.setMode,
        zoomIn: this.zoomIn,
        zoomOut: this.zoomOut,
      },
      watch: {
        zoomFactor: newFactor => {
          this.updateTimelineWidth(newFactor);
          /**
           * Fires when the zoom factor gets changed.
           * @event Editor#zoomChange
           * @param {number} newFactor - New zoom factor.
           */
          this.emit('zoomChange', newFactor);
        },
        noteHeight: () => requestAnimationFrame(() => {
          this.canvas.updateRendererSize();
        }),
      },
    });

    this.pianoRoll = new PianoRoll(this, el.querySelector('.uv-pianoRoll'));
    this.canvas = new EditorCanvas(this);
  }

  /**
   * Set the editor mode.
   * @param {string} mode - New Mode
   */
  setMode(mode) {
    this.mode = mode;
    µV.common.showTooltip(`${mode} Mode.`);
  }

  /**
   * Update the timeline width.
   * Must be called after inserting or deleting notes.
   * @param {number} [zoomFactor=this.zoomFactor] - Current zoom factor
   */
  updateTimelineWidth(zoomFactor = this.zoomFactor) {
    // TODO: Optimize this.
    let baseWidth = 100;
    for (const noteObject of this.project.noteObjects) {
      const n = noteObject.offset + noteObject.width;
      if (n > baseWidth) baseWidth = n;
    }
    this.timelineWidth = baseWidth * zoomFactor;
  }

  /** Increases the Zoom */
  zoomIn() {
    if (this.zoomFactor >= 4.9) return;
    this.zoomFactor += 0.1;
    µV.common.showTooltip(`Zoom ${this.zoomFactor.toFixed(1)}x.`);
  }

  /** Decreases the Zoom */
  zoomOut() {
    if (this.zoomFactor <= 0.25) return;
    this.zoomFactor -= 0.1;
    µV.common.showTooltip(`Zoom ${this.zoomFactor.toFixed(1)}x.`);
  }

  /** Register events and handlers. */
  registerEvents() {
    $('#uv-logo').click(() => {
      $('#uv-main-sidebar').sidebar('toggle');
      /**
       * Fires when the sidebar is toggled (click on µV logo).
       * @event Editor#sidebarToggle
       */
      this.emit('sidebarToggle');
    });
  }
}
