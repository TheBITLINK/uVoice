import Runtime from '../base';

/**
 * Browser Runtime.
 * @extends Runtime
 */
export default class BrowserRuntime extends Runtime
{
  /** Initializes the runtime */
  init() {
    document.body.className = 'browser';
  }
}
