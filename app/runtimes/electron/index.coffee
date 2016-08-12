RuntimeBase = require '../base'
remote = (require 'electron').remote

class ElectronRuntime extends RuntimeBase
  constructor: ->
    @win = remote.getCurrentWindow()
    @init()

  init: =>
    @titleBarEvents()
    document.body.className += ' desktop'
    document.body.className += ' win32' if navigator.appVersion.includes 'Win'
    document.body.className += ' linux' if navigator.appVersion.includes 'Linux'
    document.body.className += ' osx'   if navigator.appVersion.includes 'Mac'
    document.body.className += ' x11'   if navigator.appVersion.includes 'X11'

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
      

  minimizeWindow: => @win.minimize()
  maximizeWindow: => if @win.isMaximized() then @win.unmaximize() else @win.maximize()
  closeWindow:    => @win.close()

module.exports = ElectronRuntime
