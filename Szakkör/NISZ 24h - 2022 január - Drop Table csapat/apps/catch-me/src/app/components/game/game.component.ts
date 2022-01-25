import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'catch-me-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
  @Input() roomName = 'chat';
  @Input() userName = 'dummy';
  roomid = 0;
  errorText!: string;
  
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.roomid = Number(this.route.snapshot.queryParams['id'] || '0');


  }

  moveError(text: string) {
    this.errorText = text;
    setTimeout(() => {
      this.errorText = "";
    }, 3000);
  }
}
