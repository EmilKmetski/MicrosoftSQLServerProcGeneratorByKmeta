                          SELECT   DENSE_RANK() OVER ( ORDER BY [T].[name] )                                                                                AS [Ranking]
                                  ,[T].[name]                                                                                                               AS [TableName]
                                  ,[C].[name]                                                                                                               AS [ColumnName]
                                  ,CASE
                                   WHEN Isnull([IX].[is_primary_key],0) = 1
                                   THEN 'true'
                                   ELSE 'false'
                                    END                                                                                                                     AS [isPrimaryKey]
                                  ,UPPER(
                                         [S].[name] +
                                                     CASE
                                                     WHEN [S].[name] in ('NVARCHAR','VARCHAR','DECIMAL','NUMERIC')
                                                     THEN ' (' +
                                                                CASE
                                                                WHEN [S].[name] in ('DECIMAL','NUMERIC')
                                                                THEN CAST([C].[precision] AS NVARCHAR(max)) +',' + CAST([C].[scale] AS NVARCHAR(max))
                                                                ELSE
                                                                    CASE
                                                                    WHEN [C].[max_length] = -1
                                                                    THEN 'MAX'
                                                                    ELSE CAST([C].[max_length] AS NVARCHAR(max))
                                                                     END
                                                                 END
                                                         + ')'
                                                     ELSE ''
                                                      END
                                        )                                                                                                                   AS [ColumnType]
                                  ,CASE
                                   WHEN [C].[is_nullable] = 1
                                   THEN 'true'
                                   ELSE 'false'
                                    END                                                                                                                     AS Nullable
                                  ,CASE
                                   WHEN [FKData].[Result] IS NOT NULL
                                   THEN 'true'
                                   ELSE 'false'
                                    END                                                                                                                     AS [IsForeignKey]
                              FROM [sys].[tables]              AS [T]
                              JOIN [sys].[columns]             AS [C]   ON [T].[object_id]         = [C].[object_id]
                              JOIN [sys].[types]               AS [S]   ON [C].[user_type_id]      = [S].[user_type_id]
                         LEFT JOIN [sys].[index_columns]       AS [IC]  ON [IC].[object_id]        = [C].[object_id]  AND [IC].[column_id]  = [C].[column_id] AND [IC].[index_id] = 1
                         LEFT JOIN [sys].[indexes]             AS [IX]  ON [IX].[object_id]        = [IC].[object_id] AND [IX].[index_id]   = [IC].[index_id]
                       OUTER APPLY (SELECT 1                           AS [Result]
                                      FROM [sys].[foreign_keys]        AS [FK]
                                      JOIN [sys].[foreign_key_columns] AS [FKC] ON [FK].[object_id]  = [FKC].[constraint_object_id] AND [FKC].[parent_column_id] = [C].[column_id]
                                     WHERE [FK].[parent_object_id] = [T].[object_id]
                                    ) FKData
                             WHERE [T].[name] NOT LIKE 'schema%'
                          ORDER BY [T].[name]
                                  ,[C].[column_id];