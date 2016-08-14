import Editor from './core/editor';
import Common from './core/common';
import BrowserRuntime from './runtimes/browser';

window.jQuery = require('jquery');

const AudioContext = window.AudioContext || window.webkitAudioContext;

class App {
  constructor() {
    this.ctx = new AudioContext();
    this.common = new Common();
    this.editor = new Editor(document.getElementById('uv-main-content'));
    this.runtime = window.runtime || new BrowserRuntime();
  }

  init() {
    this.common.init();
    this.runtime.init();
  }
}

window.µV = new App();
window.µV.init();
