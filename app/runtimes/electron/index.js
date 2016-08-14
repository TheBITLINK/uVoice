const { remote } = require('electron');
const Runtime = require('../base');

/**
 * Electron Desktop Runtime.
 * @extends Runtime
 * @property {Object} win - The Electron Window.
 */
class ElectronRuntime extends Runtime
{
  constructor() {
    super();
    this.type = 'electron';
    this.win = remote.getCurrentWindow();
  }

  /** Initializes the runtime */
  init() {
    const b = document.body;
    const v = navigator.appVersion;

    b.className = 'desktop';
    if (v.includes('Win')) b.className += ' win32';
    if (v.includes('Mac')) b.className += ' osx';
    if (v.includes('X11')) b.className += ' X11';
    if (v.includes('Linux')) b.className += ' linux';

    this.titleBarEvents();
  }

  /** Register Titlebar Events */
  titleBarEvents() {
    document.getElementById('close-btn').addEventListener('click', () => this.closeWindow());
    document.getElementById('maximize-btn').addEventListener('click', () => this.maximizeWindow());
    document.getElementById('minimize-btn').addEventListener('click', () => this.minimizeWindow());
    document.getElementById('minimize-btn').addEventListener('click', () => this.minimizeWindow());
    document.getElementById('uv-e-devtools').addEventListener('click', () => this.toggleDevTools());
    document.getElementById('uv-e-refresh').addEventListener('click', () => window.location.reload());
  }

  /** Minimize the app window */
  minimizeWindow() {
    this.win.minimize();
    return true;
  }

  /** Maximize or restore the app window */
  maximizeWindow() {
    if (this.win.isMaximized()) {
      this.win.unmaximize();
    } else {
      this.win.maximize();
    }
  }

  /** Open or close DevTools */
  toggleDevTools() {
    this.win.toggleDevTools();
  }
}

module.exports = ElectronRuntime;
