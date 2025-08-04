import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { NotificationService, Notification } from '../../services/notification.service';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="notification-container">
      <div 
        *ngFor="let notification of notifications; trackBy: trackByNotification" 
        class="notification"
        [ngClass]="notification.type"
        [@slideInOut]="'in'">
        <div class="notification-content">
          <div class="notification-icon">
            <i *ngIf="notification.type === 'success'" class="fas fa-check-circle"></i>
            <i *ngIf="notification.type === 'error'" class="fas fa-exclamation-circle"></i>
            <i *ngIf="notification.type === 'warning'" class="fas fa-exclamation-triangle"></i>
            <i *ngIf="notification.type === 'info'" class="fas fa-info-circle"></i>
          </div>
          <div class="notification-message">
            {{ notification.message }}
          </div>
          <button class="notification-close" (click)="removeNotification(notification.id)">
            <i class="fas fa-times"></i>
          </button>
        </div>
        <div class="notification-progress">
          <div 
            class="notification-progress-bar" 
            [style.animation-duration.ms]="notification.duration || 5000"></div>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .notification-container {
      position: fixed;
      top: 20px;
      right: 20px;
      z-index: 9999;
      max-width: 350px;
    }
    
    .notification {
      background: white;
      border-radius: 12px;
      box-shadow: 0 10px 30px rgba(0,0,0,0.15);
      margin-bottom: 15px;
      border-left: 4px solid;
      animation: slideIn 0.4s ease-out;
      font-size: 14px;
      font-weight: 500;
      position: relative;
      overflow: hidden;
    }
    
    .notification::before {
      content: '';
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      height: 3px;
      background: var(--primary-gradient);
    }
    
    .notification-content {
      display: flex;
      align-items: center;
      padding: 16px 20px;
    }
    
    .notification-icon {
      margin-right: 12px;
      font-size: 20px;
    }
    
    .notification-message {
      flex: 1;
      line-height: 1.5;
    }
    
    .notification-close {
      background: transparent;
      border: none;
      color: #718096;
      cursor: pointer;
      font-size: 16px;
      padding: 0;
      margin-left: 10px;
      opacity: 0.7;
      transition: all 0.2s ease;
    }
    
    .notification-close:hover {
      opacity: 1;
    }
    
    .notification-progress {
      height: 3px;
      width: 100%;
      background: rgba(0,0,0,0.05);
    }
    
    .notification-progress-bar {
      height: 100%;
      width: 100%;
      transform-origin: left;
      animation: progress linear forwards;
    }
    
    @keyframes progress {
      from { transform: scaleX(1); }
      to { transform: scaleX(0); }
    }
    
    .notification.success {
      border-left-color: #28a745;
      color: #155724;
    }
    
    .notification.success .notification-icon {
      color: #28a745;
    }
    
    .notification.success .notification-progress-bar {
      background: linear-gradient(to right, #28a745, #4facfe);
    }
    
    .notification.error {
      border-left-color: #dc3545;
      color: #721c24;
    }
    
    .notification.error .notification-icon {
      color: #dc3545;
    }
    
    .notification.error .notification-progress-bar {
      background: linear-gradient(to right, #dc3545, #ff6b6b);
    }
    
    .notification.warning {
      border-left-color: #ffc107;
      color: #856404;
    }
    
    .notification.warning .notification-icon {
      color: #ffc107;
    }
    
    .notification.warning .notification-progress-bar {
      background: linear-gradient(to right, #ffc107, #f5576c);
    }
    
    .notification.info {
      border-left-color: #17a2b8;
      color: #0c5460;
    }
    
    .notification.info .notification-icon {
      color: #17a2b8;
    }
    
    .notification.info .notification-progress-bar {
      background: linear-gradient(to right, #17a2b8, #4facfe);
    }
    
    @keyframes slideIn {
      from {
        transform: translateX(100%);
        opacity: 0;
      }
      to {
        transform: translateX(0);
        opacity: 1;
      }
    }
    
    .notification.fade-out {
      animation: fadeOut 0.3s ease-in forwards;
    }
    
    @keyframes fadeOut {
      from {
        transform: translateX(0);
        opacity: 1;
      }
      to {
        transform: translateX(100%);
        opacity: 0;
      }
    }
  `],
  animations: [
    trigger('slideInOut', [
      state('in', style({
        transform: 'translateX(0)',
        opacity: 1
      })),
      state('out', style({
        transform: 'translateX(100%)',
        opacity: 0
      })),
      transition('in => out', [
        animate('300ms ease-in')
      ]),
      transition('void => in', [
        style({ transform: 'translateX(100%)', opacity: 0 }),
        animate('300ms ease-out')
      ])
    ])
  ]
})
export class NotificationComponent implements OnInit, OnDestroy {
  notifications: Notification[] = [];
  private subscription: Subscription = new Subscription();

  constructor(private notificationService: NotificationService) {}

  ngOnInit() {
    this.subscription = this.notificationService.getNotifications().subscribe(notification => {
      this.notifications.push(notification);
      
      setTimeout(() => {
        this.removeNotification(notification.id);
      }, notification.duration || 5000);
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  removeNotification(id: number) {
    const notification = this.notifications.find(n => n.id === id);
    if (notification) {
      notification['fade-out'] = true;
      setTimeout(() => {
        this.notifications = this.notifications.filter(n => n.id !== id);
      }, 300);
    }
  }

  trackByNotification(index: number, notification: Notification): number {
    return notification.id;
  }
}