import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Color } from '../../models/color';
import { Icon } from '../../models/Icon';

@Component({
  selector: 'app-icon-selector',
  templateUrl: './icon-selector.component.html',
  styleUrls: ['./icon-selector.component.scss']
})
export class IconSelectorComponent implements OnInit {
  iconsList: Icon[];
  color: Color;
  icon: Icon;

  constructor(
    public dialogRef: MatDialogRef<IconSelectorComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) {}

  ngOnInit() {
    this.iconsList = this.data.icons;
    this.color = this.data.color;
    this.icon = this.data.icon;
  }

  setIcon(selceted: Icon) {
    this.icon = selceted;
    this.close();
  }

  private close() {
    this.dialogRef.close({icon: this.icon});
  }
}
