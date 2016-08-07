RuntimeBase = require '../base'

class BrowserRuntime extends RuntimeBase
  constructor: ()->
    @init()

  init: =>
    document.body.className += ' browser'

  closeWindow: => window.close()

module.exports = BrowserRuntime
