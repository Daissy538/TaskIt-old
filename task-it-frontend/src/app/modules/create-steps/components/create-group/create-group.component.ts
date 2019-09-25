import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { GroupService } from 'src/app/modules/group/group.service';
import { Color } from 'src/app/core/models/color';
import { Icon } from 'src/app/core/models/Icon';
import { Group } from 'src/app/core/models/group';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IconSelectorComponent } from 'src/app/core/components/icon-selector/icon-selector.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-group-generate',
  templateUrl: './create-group.component.html',
  styleUrls: ['./create-group.component.scss']
})
export class CreateGroupComponent implements OnInit {
  groupCreateForm: FormGroup;
  title: FormControl;
  description: FormControl;
  icon: FormControl;
  color: FormControl;

  colors: Color[] = [
    { value: '#ec407a', viewValue: 'Pink' },
    { value: '#ef5350', viewValue: 'Orange' },
    { value: '#ab47bc', viewValue: 'Purple' },
    { value: '#5c6bc0', viewValue: 'Blue' }
  ];

  icons: Icon[] = [
    { value: 'house', description: 'House' },
    { value: 'work', description: 'Work' },
    { value: 'directions_run', description: 'Sport' },
    { value: 'school', description: 'Education' },
    { value: 'headset_mic', description: 'Game' },
    { value: 'music_note', description: 'Music' },
    { value: 'nature_people', description: 'Nature' },
    { value: 'loyalty', description: 'Voluntary work' },
    { value: 'pets', description: 'Animal' },
    { value: 'color_lens', description: 'Art' }
  ];

  constructor(
    private formBuilder: FormBuilder,
    private groupService: GroupService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    this.title = new FormControl('', [Validators.required]);
    this.description = new FormControl();
    this.icon = new FormControl(this.icons[0]);
    this.color = new FormControl(this.colors[0]);

    this.groupCreateForm = this.formBuilder.group({
      title: this.title,
      description: this.description,
      icon: this.icon,
      color: this.color,
      floatLabel: 'auto'
    });
  }

  getErrorMessage(controller: FormControl) {
    return controller.hasError('required') ? 'Veld is verplicht' : '';
  }

  createGroup() {
    const group = new Group(
      -1,
      this.title.value,
      this.description.value,
      this.icon.value.value,
      this.color.value.value
    );

    this.groupService.createGroup(group).subscribe(response => {
      this.snackBar.open('Group is toegevoegd', 'X', {
        panelClass: ['custom-ok']
      });

      this.router.navigate(['/dashboard']);
    });
  }

  selectIcon() {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.width = '800px';
    dialogConfig.height = 'fit-content';
    dialogConfig.panelClass = 'dialog-select-icons';

    dialogConfig.data = {
      icons: this.icons,
      icon: this.icon.value,
      color: this.color.value
    };

    const dialog = this.dialog.open(IconSelectorComponent, dialogConfig);
    dialog.afterClosed().subscribe(response => {
      if (response) {
        this.icon.setValue(response.icon);
      }
    });
  }

  cancel() {
    this.router.navigate(['/dashboard']);
  }

  next() {}
}
