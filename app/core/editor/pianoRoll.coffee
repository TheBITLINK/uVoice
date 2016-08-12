class µVoicePianoRollClass
  constructor: (@el)->
    @n = {} # Notes being played
    @keyEvents();

  # Bind events to the piano keys
  keyEvents: =>
    $(@el).on 'mousedown', '.pianoKey', (e)=>
      @playNote @getNoteName(e.target.parentElement)
    $(@el).on 'mouseup', '.pianoKey', (e)=> @stopAll()
  
  getNoteName: (note)=>
    name = note.dataset.n # Note
    name += '#' if 'sharp' in note.classList # Sharp?
    name += note.dataset.o # Octave

  playNote: (name)=>
    o = @n[name] = µV.ctx.createOscillator()
    o.type = 'square'
    o.frequency.value = µV.common.notes[name]
    o.connect µV.ctx.destination
    o.start 0
  
  stopNote: (name)=>
    @n[name].stop 0
    delete @n[name]

  stopAll: => @stopNote name for name in Object.keys(@n)

module.exports = µVoicePianoRollClass
