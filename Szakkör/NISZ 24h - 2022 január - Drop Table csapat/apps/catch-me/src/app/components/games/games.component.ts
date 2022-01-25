import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService, GamesService } from '../../services';



interface PlayerNamesInterface {
  lobby_id:string;
  admin_name: string;
  user_names:string;
}

@Component({
  selector: 'catch-me-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})

export class GamesComponent implements OnInit {
  roomName = 'chat';
  userName = 'dummy';
  games:PlayerNamesInterface[] = [];


  constructor(
    private readonly authenticationService: AuthenticationService,
    private gamesService: GamesService,
    private router: Router) { }
  

  ngOnInit(): void {

    this.gamesService.quitAll().subscribe(next=>{
      console.log(next);
    });
    this.refresh();
    
    const currentUser = this.authenticationService.currentUser;
    if (currentUser) {
      this.userName = currentUser.name;
    }
  }
  
  refresh() {
    this.gamesService.getAll().subscribe(next=>{
      if (next[0].lobby_id) {
        this.games = [];
        this.games = next;
      }
      console.log(next);
    });
  }

  public CreateLobby(){
    this.gamesService.create().subscribe(next=>{
      this.games.push(next);
      console.log(next);
    });
  }

  deleteGame(game: any): void {
    const index = this.games.indexOf(game);
    this.games.splice(index, 1);

  }

  connectIn(game: PlayerNamesInterface){
    this.router.navigateByUrl(`/game?id=${game.lobby_id}`);    
  }
  

}
