const { app, BrowserWindow } = require('electron');

let win = null;

app.on('ready', () => {
  win = new BrowserWindow({
    width: 1024,
    height: 600,
    minWidth: 721,
    minHeight: 64,
    overlayScrollbars: true,
    frame: false,
    transparent: true,
  });
  win.loadURL(`file://${__dirname}/../build/index.html`);
  win.on('closed', () => { win = null; });
});

app.on('window-all-closed', () => {
  app.quit();
});
