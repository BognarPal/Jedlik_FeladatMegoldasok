import { Component, OnInit, AfterViewInit, Input } from '@angular/core';
import { Router } from '@angular/router';
declare let JitsiMeetExternalAPI: any;

@Component({
  selector: 'catch-me-meet',
  templateUrl: './meet.component.html',
  styleUrls: ['./meet.component.scss'],
})
export class MeetComponent implements AfterViewInit {
  @Input() roomName = '';
  @Input() userName = '';

  api: any;
  isAudioMuted = false;
  isVideoMuted = false;

  constructor(private router: Router) {}

  ngAfterViewInit(): void {
    console.log(this.userName);
    this.api = new JitsiMeetExternalAPI('meet.jit.si', {
      roomName: this.roomName,
      width: 400,
      height: 800,
      configOverwrite: { prejoinPageEnabled: false },
      interfaceConfigOverwrite: {},
      parentNode: document.querySelector('#jitsi-iframe'),
      userInfo: {
        displayName: this.userName,
      },
    });

    this.api.addEventListeners({
      readyToClose: this.handleClose,
      participantLeft: this.handleParticipantLeft,
      participantJoined: this.handleParticipantJoined,
      videoConferenceJoined: this.handleVideoConferenceJoined,
      videoConferenceLeft: this.handleVideoConferenceLeft,
      audioMuteStatusChanged: this.handleMuteStatus,
      videoMuteStatusChanged: this.handleVideoStatus,
    });
  }

  handleClose = () => {
    console.log('handleClose');
  };

  handleParticipantLeft = async (participant: any) => {
    console.log('handleParticipantLeft', participant);
    const data = await this.getParticipants();
  };

  handleParticipantJoined = async (participant: any) => {
    console.log('handleParticipantJoined', participant);
    const data = await this.getParticipants();
  };

  handleVideoConferenceJoined = async (participant: any) => {
    console.log('handleVideoConferenceJoined', participant);
    const data = await this.getParticipants();
  };

  handleVideoConferenceLeft = () => {
    console.log('handleVideoConferenceLeft');
    this.router.navigate(['/thank-you']);
  };

  handleMuteStatus = (audio: any) => {
    console.log('handleMuteStatus', audio);
  };

  handleVideoStatus = (video: any) => {
    console.log('handleVideoStatus', video);
  };

  getParticipants() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(this.api.getParticipantsInfo());
      }, 500);
    });
  }

  executeCommand(command: string) {
    this.api.executeCommand(command);
    if (command == 'hangup') {
      this.router.navigate(['/thank-you']);
      return;
    }

    if (command == 'toggleAudio') {
      this.isAudioMuted = !this.isAudioMuted;
    }

    if (command == 'toggleVideo') {
      this.isVideoMuted = !this.isVideoMuted;
    }
  }
}
