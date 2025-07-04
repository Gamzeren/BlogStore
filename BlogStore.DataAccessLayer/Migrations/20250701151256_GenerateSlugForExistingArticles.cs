using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogStore.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class GenerateSlugForExistingArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Mevcut veriler için slug üret
            migrationBuilder.Sql(@"
        UPDATE Articles 
        SET Slug = LOWER(
            REPLACE(
                REPLACE(
                    REPLACE(
                        REPLACE(
                            REPLACE(
                                REPLACE(
                                    REPLACE(
                                        REPLACE(
                                            REPLACE(
                                                REPLACE(
                                                    REPLACE(
                                                        REPLACE(
                                                            REPLACE(
                                                                REPLACE(Title, ' ', '-'),
                                                            'ı', 'i'),
                                                        'ğ', 'g'),
                                                    'ü', 'u'),
                                                'ş', 's'),
                                            'ö', 'o'),
                                        'ç', 'c'),
                                    'İ', 'i'),
                                'Ğ', 'g'),
                            'Ü', 'u'),
                        'Ş', 's'),
                    'Ö', 'o'),
                'Ç', 'c'),
            '.', '')
        )
        WHERE Slug IS NULL OR Slug = '';
    ");

            migrationBuilder.Sql(@"
        UPDATE Articles 
        SET Slug = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Slug, '!', ''), '?', ''), ',', ''), ':', ''), ';', '')
        WHERE Slug LIKE '%[!?,:;]%';
    ");

            migrationBuilder.Sql(@"
        UPDATE Articles 
        SET Slug = REPLACE(REPLACE(REPLACE(Slug, '---', '-'), '--', '-'), '--', '-')
        WHERE Slug LIKE '%-%';
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
