import EventEmitter from 'events';

/**
 * An Event Forwarder for Common Events.
 * @fires µV.windowResize
 */
export default class CommonEvents extends EventEmitter {
  constructor() {
    super();
    /**
     * Fires when the app's window is resized.
     * @event µV#windowResize
     */
    window.addEventListener('resize', e => this.emit('windowResize', e));
  }
}
