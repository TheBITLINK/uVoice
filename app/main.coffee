µVEditor = require './core/editor'
µVCommon = require './core/common'
BrowserRuntime = require './runtimes/browser'
AudioContext = window.AudioContext or window.webkitAudioContext

class µVoice
  constructor: ->
    @ctx     = new AudioContext()
    @common  = new µVCommon()
    @editor  = new µVEditor()
    @runtime = window.runtime or new BrowserRuntime()

window.µV = new µVoice()