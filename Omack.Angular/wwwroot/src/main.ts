import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { TestModule } from './app/test/test.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}
//enableProdMode(); uncomment this when deplyoing
platformBrowserDynamic().bootstrapModule(AppModule);
platformBrowserDynamic().bootstrapModule(TestModule);
