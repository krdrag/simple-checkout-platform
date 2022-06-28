﻿using MassTransit;
using SCP.Transaction.Application.Saga.Events;
using SCP.Transaction.Domain.Constants;

namespace SCP.Transaction.Application.Saga
{
    public static class TransactionSagaOperations
    {
        public static Action<BehaviorContext<TransactionSagaState, IOnTransactionStarted>> CreateTransaction()
        {
            return context =>
            {
                context.Saga.Transaction = new Models.TransactionModel
                {
                    TransactionId = context.Message.TransactionId,
                    TimeStarted = DateTime.Now,
                    WorkstationData = context.Message.WorkstationData,
                    State = TransactionConstants.State.Started
                };
            };
        }

        public static Action<BehaviorContext<TransactionSagaState, IOnTransactionUpdated>> AddArticles()
        {
            return context =>
            {
                foreach (var article in context.Message.ArticlesToAdd)
                {
                    article.LineNumber = ++context.Saga.CurrentLineNumber;
                    context.Saga.Transaction.Articles.Add(article);
                }
            };
        }

        public static Action<BehaviorContext<TransactionSagaState, IOnTransactionUpdated>> SwtichToTotal()
        {
            return context =>
            {
                context.Saga.Transaction.State = TransactionConstants.State.Total;
            };
        }

        public static Action<BehaviorContext<TransactionSagaState, IOnTransactionUpdated>> AddPayments()
        {
            return context =>
            {
                foreach (var payment in context.Message.PaymentsToAdd)
                {
                    payment.LineNumber = ++context.Saga.CurrentLineNumber;
                    context.Saga.Transaction.Payments.Add(payment);
                }
            };
        }
    }
}
