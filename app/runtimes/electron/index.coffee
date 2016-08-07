RuntimeBase = require '../base'
remote = (require 'electron').remote

class ElectronRuntime extends RuntimeBase
  constructor: ->
    @win = remote.getCurrentWindow()
    @init()

  init: =>
    @titleBarEvents()
    document.body.className += ' desktop'

  titleBarEvents: =>
    # Close Button
    document.getElementById 'close-btn'
    .addEventListener 'click', @closeWindow
    # Maximize Button
    document.getElementById 'maximize-btn'
    .addEventListener 'click', @maximizeWindow
    # Minimize Button
    document.getElementById 'minimize-btn'
    .addEventListener 'click', @minimizeWindow
    # Hide Maximize on Linux
    if navigator.platform is 'Linux x86_64'
      document.getElementById 'maximize-btn'
      .style = 'display:none;'

  minimizeWindow: => @win.minimize()
  maximizeWindow: => if @win.isMaximized() then @win.unmaximize() else @win.maximize()
  closeWindow:    => @win.close()

module.exports = ElectronRuntime
