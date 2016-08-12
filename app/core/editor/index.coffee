µVoicePianoRoll = require './pianoRoll'
$ = require 'jquery'

class µVoiceEditorClass
  constructor: ->
    @pianoRoll = new µVoicePianoRoll $('.uv-pianoRoll')[0]
    @sidebarEvents()

  sidebarEvents:=>
    $('#uv-logo').click => $('#uv-main-sidebar').sidebar 'toggle'

module.exports = µVoiceEditorClass
