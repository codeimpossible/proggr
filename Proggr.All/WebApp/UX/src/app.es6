import {default as injectTapEventPlugin} from 'react-tap-event-plugin';

import AppRouter from './router';

require('./styles/app.css');

// a very bare-bones application object for our
// react codebase
class App {
  constructor() {
    injectTapEventPlugin();
    AppRouter.initializeApplicationRoutes();
  }
}

export default new App();
