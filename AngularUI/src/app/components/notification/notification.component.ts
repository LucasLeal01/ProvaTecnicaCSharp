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
        [ngClass]="notification.type">
        <div class="notification-header">
          <span class="notification-icon">
            <span *ngIf="notification.type === 'success'">✓</span>
            <span *ngIf="notification.type === 'error'">✕</span>
            <span *ngIf="notification.type === 'warning'">⚠</span>
            <span *ngIf="notification.type === 'info'">ℹ</span>
          </span>
          <span class="notification-title">
            <span *ngIf="notification.type === 'success'">Sucesso</span>
            <span *ngIf="notification.type === 'error'">Erro</span>
            <span *ngIf="notification.type === 'warning'">Aviso</span>
            <span *ngIf="notification.type === 'info'">Informação</span>
          </span>
        </div>
        <div class="notification-body">
          {{ notification.message }}
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
      max-width: 400px;
      min-width: 320px;
    }
    
    .notification {
      background: white;
      border-radius: 8px;
      box-shadow: 0 4px 20px rgba(0,0,0,0.15);
      margin-bottom: 12px;
      border: 1px solid #e0e0e0;
      overflow: hidden;
      transition: all 0.3s ease;
    }
    
    .notification:hover {
      transform: translateY(-2px);
      box-shadow: 0 8px 30px rgba(0,0,0,0.2);
    }
    
    .notification-header {
      display: flex;
      align-items: center;
      padding: 12px 16px;
      border-bottom: 1px solid #f0f0f0;
      font-weight: 600;
      font-size: 14px;
    }
    
    .notification-icon {
      margin-right: 8px;
      font-size: 16px;
      font-weight: bold;
    }
    
    .notification-title {
      text-transform: uppercase;
      font-size: 12px;
      letter-spacing: 0.5px;
    }
    
    .notification-body {
      padding: 16px;
      font-size: 14px;
      line-height: 1.5;
      color: #333;
    }
    
    .notification.success {
      border-left: 4px solid #10b981;
    }
    
    .notification.success .notification-header {
      background: #f0fdf4;
      color: #065f46;
    }
    
    .notification.success .notification-icon {
      color: #10b981;
    }
    
    .notification.error {
      border-left: 4px solid #ef4444;
    }
    
    .notification.error .notification-header {
      background: #fef2f2;
      color: #991b1b;
    }
    
    .notification.error .notification-icon {
      color: #ef4444;
    }
    
    .notification.warning {
      border-left: 4px solid #f59e0b;
    }
    
    .notification.warning .notification-header {
      background: #fffbeb;
      color: #92400e;
    }
    
    .notification.warning .notification-icon {
      color: #f59e0b;
    }
    
    .notification.info {
      border-left: 4px solid #3b82f6;
    }
    
    .notification.info .notification-header {
      background: #eff6ff;
      color: #1e40af;
    }
    
    .notification.info .notification-icon {
      color: #3b82f6;
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

  onAnimationDone(event: any, notification: Notification) {
    // Lógica adicional se necessário
  }

  trackByNotification(index: number, notification: Notification): number {
    return notification.id;
  }
} 