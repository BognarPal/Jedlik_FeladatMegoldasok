import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GamesService } from '../../../services';

interface PlayerNamesInterface {
  lobby_id:string;
  admin_name: string;
  user_names:string;
}

@Component({
  selector: 'catch-me-lobby',
  templateUrl: './lobby.component.html',
  styleUrls: []
})

export class LobbyComponent implements OnInit {

  @Input() userName = "";
  @Input() gamee:PlayerNamesInterface = {lobby_id:"unknown",admin_name:"unknown",user_names:"unknown"};
  @Output() deleteGame = new EventEmitter();
  @Output() connectIn = new EventEmitter();

  constructor(
    private gamesService: GamesService) { 
  }

  ngOnInit(): void {
    console.log(this.gamee);
  }



  public LogintoLobby(game:any){
    this.gamesService.join(Number(game.lobby_id)).subscribe(res=>{
      console.log(res);
      
    });
    this.connectIn.emit(game);
  }

  
  
  public QuitLobby(game:PlayerNamesInterface){
    this.gamesService.quit(Number(game.lobby_id)).subscribe(next=>{
      console.log(next);
    });
  }

  
  public DeleteLobby(game:PlayerNamesInterface){
    this.gamesService.del(Number(game.lobby_id)).subscribe(next=>{
      if (next.affected == 1) {
        this.deleteGame.emit(game);
      }
      console.log(next);
    });
  }

}
