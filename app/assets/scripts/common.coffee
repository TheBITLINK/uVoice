jQuery = require 'jquery'
window.$ = window.jQuery = jQuery

(()->
  remote = (require 'electron').remote

  class µVoiceCommonClass
    constructor: ->
      @win = remote.getCurrentWindow()
      @init()
  
    init: =>
      @titleBarEvents()

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

    showTooltip: (text)=>
      tooltip = $('<div class="overlayTooltip"></div>')
      .text text
      .appendTo '#uv-overlay'
      setTimeout (->tooltip.remove()), 2100

  window.µVCommon = new µVoiceCommonClass()
)()

$('.uv-toolbar').on 'click', 'button', -> µVCommon.showTooltip 'Not yet supported.'