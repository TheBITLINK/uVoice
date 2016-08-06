{app, BrowserWindow} = require 'electron'
win = undefined

app.on 'ready', ->
  win = new BrowserWindow {
      width: 1024
      height: 600
      frame: false
      transparent: true
  }
  win.loadURL "file://#{__dirname}/view/main.jade"
  win.on 'closed', -> win=null

app.on 'window-all-closed', -> app.quit() if process.platform isnt 'darwin'
