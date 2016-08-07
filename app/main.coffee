µVEditor = require './core/editorMain'
µVCommon = require './core/common'
BrowserRuntime = require './runtimes/browser'

class µVoice
  constructor: ->
    @common  = new µVCommon()
    @editor  = new µVEditor()
    @runtime = window.runtime or new BrowserRuntime()

window.µV = new µVoice()