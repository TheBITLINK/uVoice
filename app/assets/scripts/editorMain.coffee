µVoicePianoRoll = require './pianoRoll'
$ = require 'jquery'

class µVoiceEditorClass
  constructor: ->
    @pianoRoll = new µVoicePianoRoll()
    @sidebarEvents()

  sidebarEvents:=>
    $('#uv-logo').click => $('#uv-main-sidebar').sidebar 'toggle'

module.exports = µVoiceEditorClass