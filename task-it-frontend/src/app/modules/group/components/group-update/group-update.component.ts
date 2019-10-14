import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  HostListener,
  SimpleChanges
} from '@angular/core';
import { GroupOutgoing, GroupIncoming } from 'src/app/core/models/group';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { Icon } from 'src/app/core/models/Icon';
import { Color } from 'src/app/core/models/color';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { IconSelectorComponent } from 'src/app/core/components/icon-selector/icon-selector.component';

@Component({
  selector: 'app-group-update',
  templateUrl: './group-update.component.html',
  styleUrls: ['./group-update.component.scss']
})
export class GroupUpdateComponent implements OnInit, OnChanges {
  @Input()
  group: GroupIncoming;
  @Input()
  icons: Icon[];
  @Input()
  colors: Color[];
  @Output()
  onUpdate = new EventEmitter<GroupOutgoing>();

  iconSelected: Icon;
  colorSelected: Color;

  inWriteMode: boolean;

  groupUpdateForm: FormGroup;
  title: FormControl;
  description: FormControl;
  icon: FormControl;
  color: FormControl;

  constructor(private formBuilder: FormBuilder, private dialog: MatDialog) {}

  ngOnInit() {
    this.setInReadMode();

    this.onUpdate.subscribe(response => {
      this.setInReadMode();
    });

    this.title = new FormControl('', [Validators.required]);
    this.description = new FormControl();
    this.icon = new FormControl();
    this.color = new FormControl();

    this.groupUpdateForm = this.formBuilder.group({
      title: this.title,
      description: this.description,
      icon: this.icon,
      color: this.color,
      floatLabel: 'auto'
    });
  }

  ngOnChanges() {
    this.resetController();
  }

  setInWriteMode() {
    this.inWriteMode = true;
  }

  setInReadMode() {
    this.inWriteMode = false;
    this.resetController();
  }

  setMode() {
    if (this.inWriteMode) {
      this.setInReadMode();
    } else {
      this.setInWriteMode();
    }
  }

  update() {
    const updateGroup = new GroupOutgoing(
      this.group.id,
      this.title.value,
      this.description.value,
      this.iconSelected.id,
      this.colorSelected.id
    );

    this.onUpdate.emit(updateGroup);
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
        this.iconSelected = response.icon;
      }
    });
  }

  selectColor(event) {
    this.colorSelected = event.value;
  }

  private findIcon(icons: Icon[], value: string): Icon {
    let icon = null;
    icons.forEach(i => {
      if (i.value === value) {
        return (icon = i);
      }
    });

    return icon;
  }

  private findColor(colors: Color[], value: string): Color {
    let color = null;

    colors.forEach(c => {
      if (c.value === value) {
        return (color = c);
      }
    });

    return color;
  }

  private resetController() {
    if (this.group && this.icons && this.colors) {
      this.iconSelected = this.findIcon(this.icons, this.group.iconValue);
      this.colorSelected = this.findColor(this.colors, this.group.colorValue);

      this.title.setValue(this.group.name);
      this.description.setValue(this.group.description);
      this.icon.setValue(this.iconSelected);
      this.color.setValue(this.colorSelected);
    }
  }
}
