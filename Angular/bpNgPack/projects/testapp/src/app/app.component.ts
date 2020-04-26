import { BPModalService } from './../../../bp-modal/src/lib/bp-modal.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'testapp';
  visible = false;

  constructor(private bpModalService: BPModalService) { }

  confirm() {
    this.bpModalService.confirm('Megerősítés', 'Biztos???', 'Igen', 'Nem', 'lg' )
    .then((confirmed) => console.log('User confirmed:', confirmed))
    .catch(() => console.log('User dismissed the dialog'));
  }

}
