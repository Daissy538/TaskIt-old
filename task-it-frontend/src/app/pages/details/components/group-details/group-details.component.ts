import { Component, OnInit, HostListener } from '@angular/core';
import { GroupOutgoing, GroupIncoming } from 'src/app/core/models/group';
import { GroupService } from 'src/app/modules/group/group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Color } from 'src/app/core/models/color';
import { Icon } from 'src/app/core/models/Icon';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.scss']
})
export class GroupDetailsComponent implements OnInit {
  group: GroupIncoming;

  colors: Color[];
  icons: Icon[];

  screenWidth: number;

  constructor(
    private groupService: GroupService,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.screenWidth = window.innerWidth;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const groupId = params['id'];
      this.getGroupDetails(groupId);
    });

    this.initDefaults();
    this.screenWidth = window.innerWidth;
  }

  updateGroup(event: GroupOutgoing) {
    this.groupService.updateGroup(event).subscribe(response => {
      this.group = response;
      this.snackBar.open('De wijzigingen zijn opgeslagen', 'X', {
        panelClass: ['custom-ok']
      });
      return;
    }, error => {
      return;
    });
  }

  getGroupDetails(groupId: number) {
    this.groupService.getGroupByID(groupId).subscribe(response => {
      this.group = response;
    });
  }

  previousPage() {
    this.router.navigate(['/dashboard']);
  }

  /**
   * Init the default values of colors and icons.
   */
  private initDefaults() {
    this.groupService.getDefaultColors().subscribe(response => {
      this.colors = response;
    });

    this.groupService.getDefaultIcons().subscribe(response => {
      this.icons = response;
    });
  }
}
