import {default as injectTapEventPlugin} from 'react-tap-event-plugin';
import {AppRouter} from './router';

require('./styles/app.css');

injectTapEventPlugin();
AppRouter.initializeRoutes();