import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailsRouting } from './details.routing';
import { GroupDetailsComponent } from './components/group-details/group-details.component';
import { GroupModule } from 'src/app/modules/group/group.module';
import { MatIconModule } from '@angular/material/icon';
import { CoreModule } from 'src/app/core/core.module';
import { UserModule } from 'src/app/modules/user/user.module';

@NgModule({
  imports: [
    CommonModule,
    GroupModule,
    UserModule,
    MatIconModule,
    DetailsRouting,
    CoreModule
  ],
  declarations: [GroupDetailsComponent]
})
export class DetailsModule { }
