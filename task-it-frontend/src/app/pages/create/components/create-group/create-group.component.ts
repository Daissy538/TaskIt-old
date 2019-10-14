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
import { GroupOutgoing } from 'src/app/core/models/group';
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

  selectedColor: Color;
  selectedIcon: Icon;
  colors: Color[] = [];
  icons: Icon[] = [];

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
    this.icon = new FormControl();
    this.color = new FormControl();

    this.groupCreateForm = this.formBuilder.group({
      title: this.title,
      description: this.description,
      icon: this.icon,
      color: this.color,
      floatLabel: 'auto'
    });

    this.initDefaults();
  }

  getErrorMessage(controller: FormControl) {
    return controller.hasError('required') ? 'Veld is verplicht' : '';
  }

  createGroup() {
    const group = new GroupOutgoing(
      -1,
      this.title.value,
      this.description.value,
      this.selectedIcon.id,
      this.selectedColor.id
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
        this.selectedIcon = response.icon;
      }
    });
  }

  selectColor(event) {
    this.selectedColor = event.value;
  }

  cancel() {
    this.router.navigate(['/dashboard']);
  }

  next() {}

  /**
   * Init the default values of colors and icons.
   */
  private initDefaults() {
    this.groupService.getDefaultColors().subscribe(response => {
      this.colors = response;
      this.selectedColor = this.colors[0];
      this.color.setValue(this.selectedColor);
    });

    this.groupService.getDefaultIcons().subscribe(response => {
      this.icons = response;
      this.selectedIcon = this.icons[0];
      this.icon.setValue(this.selectedIcon);
    });
  }
}
