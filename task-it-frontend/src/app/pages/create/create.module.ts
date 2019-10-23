import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core/core.module';
import { CreateGroupComponent } from './components/create-group/create-group.component';
import { CreateRouting } from './create.routing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatDialogModule} from '@angular/material/dialog';
import { GroupModule } from 'src/app/modules/group/group.module';



@NgModule({
  imports: [
    MatDialogModule,
    CreateRouting,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    GroupModule,
    CoreModule
  ],
  declarations: [CreateGroupComponent]
})
export class CreateModule { }
