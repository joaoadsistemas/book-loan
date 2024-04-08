import { Component, OnInit } from '@angular/core';
import { SystemService } from '../../_services/system.service';
import { IDashboard } from '../../_models/IDashboard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  dashBoard?: IDashboard;

  constructor(private systemService: SystemService) {}
  ngOnInit(): void {
    this.findDashBoard();
  }

  findDashBoard() {
    this.systemService.dashboard().subscribe({
      next: (response: IDashboard) => {
        this.dashBoard = response;
      },
    });
  }
}
