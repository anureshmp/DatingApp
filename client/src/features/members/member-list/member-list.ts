import { Component, inject } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { Observable } from 'rxjs';
import { member } from '../../../types/member';
import { AsyncPipe } from '@angular/common';

@Component({
  imports: [AsyncPipe],
  selector: 'app-member-list',
  styleUrl: './member-list.css',
  templateUrl: './member-list.html',
})
export class MemberList {
  private memberService = inject(MemberService);
  protected members$: Observable<member[]>;
  
  constructor() {
    this.members$ = this.memberService.getMembers();
  }
  
}
