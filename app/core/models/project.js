/**
 * Represents an µVoice Project.
 * @property {string} name - The Project name
 * @property {string} author - The author's username
 * @property {Date} created - The timestamp of project creation
 * @property {Date} modified - The timestamp of project modification
 * @property {string} version - The µV semver in which the project was created
 * @property {number} tempo
 * @property {string} quantize
 * @property {string} qlength
 * @property {NoteObject[]} noteObjects - The note objects in the project
 */
export default class Project {
  /**
   * Instantiates a new Project.
   * @param {Object} data - The data to load (if any)
   */
  constructor(data = {}) {
    this.name = 'New Project';
    this.author = '';
    this.created = new Date();
    this.modified = new Date();
    this.version = '^0.1';
    this.tempo = 120;
    this.quantize = '1/4';
    this.qlength = '1/4';
    this.noteObjects = [];
    Object.assign(this, data);
  }
}
