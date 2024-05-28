import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';  // Import FormsModule

import { AppComponent } from './app.component';
import { DatabindComponent } from './databind/databind.component';
import { CommunicationComponent } from './communication/communication.component';

@NgModule({
  declarations: [
    AppComponent,
    DatabindComponent,
    CommunicationComponent  // Declare the DatabindComponent
  ],
  imports: [
    BrowserModule,
    FormsModule  // Add FormsModule to imports
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
