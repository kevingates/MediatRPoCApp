using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public interface ISpecification<T, TResult>
{
	Expression<Func<T, bool>> Criteria { get; }
	List<Expression<Func<T, object>>> Includes { get; }
	Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; }
	Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending { get; }
	Expression<Func<T, TResult>> Select { get; }
	int? Skip { get; }
	int? Take { get; }

	ISpecification<T, TResult> Include(Expression<Func<T, object>> includeExpression);
	ISpecification<T, TResult> OrderByDescendingExpression(Func<IQueryable<T>, IOrderedQueryable<T>> orderByDescendingExpression);
	ISpecification<T, TResult> OrderByExpression(Func<IQueryable<T>, IOrderedQueryable<T>> orderByExpression);
	ISpecification<T, TResult> Paging(int skip, int take);
	ISpecification<T, TResult> SelectExpression(Expression<Func<T, TResult>> selectExpression);
	ISpecification<T, TResult> Where(Expression<Func<T, bool>> criteria);
}

internal class Specification<T, TResult> : ISpecification<T, TResult>
{
	public Expression<Func<T, bool>> Criteria { get; private set; }
	public List<Expression<Func<T, object>>> Includes { get; } = new();
	public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; private set; }
	public Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending { get; private set; }
	public Expression<Func<T, TResult>> Select { get; private set; }
	public int? Take { get; private set; }
	public int? Skip { get; private set; }

	public ISpecification<T, TResult> Where(Expression<Func<T, bool>> criteria)
	{
		Criteria = criteria;
		return this;
	}

	public ISpecification<T, TResult> Include(Expression<Func<T, object>> includeExpression)
	{
		Includes.Add(includeExpression);
		return this;
	}

	public ISpecification<T, TResult> OrderByExpression(Func<IQueryable<T>, IOrderedQueryable<T>> orderByExpression)
	{
		OrderBy = orderByExpression;
		return this;
	}

	public ISpecification<T, TResult> OrderByDescendingExpression(Func<IQueryable<T>, IOrderedQueryable<T>> orderByDescendingExpression)
	{
		OrderByDescending = orderByDescendingExpression;
		return this;
	}

	public ISpecification<T, TResult> SelectExpression(Expression<Func<T, TResult>> selectExpression)
	{
		Select = selectExpression;
		return this;
	}

	public ISpecification<T, TResult> Paging(int skip, int take)
	{
		Skip = skip;
		Take = take;
		return this;
	}
}
