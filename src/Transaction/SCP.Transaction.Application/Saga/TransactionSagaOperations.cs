﻿using MassTransit;
using SCP.Transaction.Application.Models;
using SCP.Transaction.Application.Saga.Events;
using SCP.Transaction.Domain.Constants;

namespace SCP.Transaction.Application.Saga
{
    public static class TransactionSagaOperations
    {
        private const decimal Tax = 0.23m;

        public static Action<BehaviorContext<TransactionSagaState, IOnTransactionStarted>> CreateTransaction()
        {
            return context =>
            {
                context.Saga.Transaction = new TransactionModel
                {
                    TransactionId = context.Message.TransactionId,
                    Sessionid = context.Message.SessionId,
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
                if (context.Message.ArticlesToAdd == null)
                    return;

                foreach (var articleData in context.Message.ArticlesToAdd)
                {
                    var article = context.Saga.Transaction.Articles.FirstOrDefault(x => x.ArticleData.EAN.Equals(articleData.EAN));
                    if(article == null)
                    {
                        article = new ArticleModel
                        {
                            ArticleData = articleData,
                            LineNumber = ++context.Saga.CurrentLineNumber
                        };
                        context.Saga.Transaction.Articles.Add(article);
                    }

                    article.Quantity++;
                    article.TotalPrice += articleData.Price; 
                }

                RecalcaulateTotal(context.Saga.Transaction);
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
                if (context.Message.PaymentsToAdd == null)
                    return;

                var total = context.Saga.Transaction.Total;

                foreach (var payment in context.Message.PaymentsToAdd)
                {
                    payment.LineNumber = ++context.Saga.CurrentLineNumber;
                    context.Saga.Transaction.Payments.Add(payment);

                    total.TotalPaid += payment.Amount;
                }
            };
        }

        public static bool IsTotalPaid(TransactionModel transaction)
            => transaction.Total.Total == transaction.Total.TotalPaid;

        private static void RecalcaulateTotal(TransactionModel transaction)
        {
            var total = transaction.Total;

            foreach(var article in transaction.Articles)
            {
                total.Total += article.TotalPrice;
                total.TaxAmount = total.Total * Tax;
                total.NetAmount = total.Total - total.TaxAmount;
            }
        }
    }
}
