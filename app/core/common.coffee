jQuery = require 'jquery'

class µVoiceCommonClass
  constructor: -> @init()

  init:=>
    window.$ = window.jQuery = jQuery
    window.µVCommon = @
    # Temporary
    $('.uv-toolbar').on 'click', 'button', -> µVCommon.showTooltip 'Not yet supported.'

  showTooltip: (text)=>
    tooltip = $('<div class="overlayTooltip"></div>')
    .text text
    .appendTo '#uv-overlay'
    setTimeout (->tooltip.remove()), 2100

module.exports = µVoiceCommonClass