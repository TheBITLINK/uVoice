/**
 * An µVoice Runtime
 * @propery {string} type - The runtime type.
 */
class Runtime {
  constructor() {
    this.type = 'base';
  }

  /** Closes the app window. */
  closeWindow() {
    window.close();
  }
}

module.exports = Runtime;
