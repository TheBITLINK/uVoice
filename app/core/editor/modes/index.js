import PanMode from './panMode';
import SelectMode from './selectMode';
import AddMode from './addMode';

export default function getModeHandlers(canvas) {
  const modes = {
    pan: new PanMode(canvas),
    select: new SelectMode(canvas),
    add: new AddMode(canvas),
  };

  canvas.stage.addChild(modes.select.gfx);
  canvas.stage.addChild(modes.add.gfx);
  return modes;
}
